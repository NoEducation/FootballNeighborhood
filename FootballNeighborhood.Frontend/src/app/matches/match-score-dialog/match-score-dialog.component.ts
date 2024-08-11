import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, UntypedFormArray, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StarRatingColor } from 'src/app/common/star-rating/star-rating.component';
import { NotificationType } from 'src/app/models/common/notification-type.constraint';
import { PlayerMatchStatusEnum } from 'src/app/models/matches/player-match-status-enum';
import { NotificationService } from 'src/app/services/communication/notification.service';
import { CurrentUserService } from 'src/app/services/current-user.service';
import { MatchPlayerReviewService } from 'src/app/services/match-player-review-service';
import { AddMatchPlayerReviewRequest } from '../models/add-match-player-review-request.model';
import { GetMatchPlayerReviewInfo } from '../models/get-match-player-review-info.model';
import { MatchScoreDialogData } from '../models/match-score-dialog-data.model';
import { GetMatchPlayerReviewsResponse } from '../models/get-match-player-reviews-response.model';
import { AddMatchPlayerReviewInfo } from '../models/add-match-player-review-info.model';

@Component({
  selector: 'app-match-score-dialog',
  templateUrl: './match-score-dialog.component.html',
  styleUrls: ['./match-score-dialog.component.scss']
})
export class MatchScoreDialogComponent implements OnInit {

  rating: number = 3;
  starCount: number = 5;
  starColor: StarRatingColor = StarRatingColor.accent;
  starColorPrimary: StarRatingColor = StarRatingColor.primary;

  matchScoreForm: FormGroup;
  dataLoading = false;
  isReadonly = false;

  get players() : any{
    return this.matchScoreForm.get('playersReviews');
  }

  constructor(
    @Inject(MAT_DIALOG_DATA) public readonly data: MatchScoreDialogData,
    private readonly formBuilder: FormBuilder,
    private readonly currentUser: CurrentUserService,
    private readonly matchPlayerReviewService: MatchPlayerReviewService,
    private readonly notificationService: NotificationService,
    public readonly matchScoreDialogReference: MatDialogRef<MatchScoreDialogComponent>) { }

  ngOnInit() {

    if(this.data.match.playerMatchStatus == PlayerMatchStatusEnum.Reviewed){
      this.matchPlayerReviewService.getMatchPlayerReviews(this.data.match.matchId).subscribe({
        next: (response) => {
          this.buildForm(response.result);
          this.matchScoreForm.disable();
          this.isReadonly = true;
        }
      });
    }
    else{
      this.buildForm();
    }
  }

  private buildForm(data: GetMatchPlayerReviewsResponse | undefined = undefined) {
    this.matchScoreForm = this.formBuilder.group({
      matchReviewScore: [data?.matchReviewScore ?? this.rating, [Validators.required]],
      matchOwnerReviewScore: [data?.matchOwnerReviewScore ?? this.rating, [Validators.required]],
      matchReviewDescription: [data?.matchReviewDescription ?? ''],
      playersReviews: this.getMatchPlayersForm(data?.playerReviews)
    });
  }

  onSubmit(): void {
    if (this.matchScoreForm.valid) {
        this.dataLoading = true;
        this.matchScoreForm.disable();

        const request : AddMatchPlayerReviewRequest = {
          matchId: this.data.match.matchId,
          matchReviewScore: this.matchScoreForm.get('matchReviewScore')?.value,
          matchOwnerReviewScore: this.matchScoreForm.get('matchOwnerReviewScore')?.value,
          matchReviewDescription: this.matchScoreForm.get('matchReviewDescription')?.value,
          playerReviews: this.getMatchPlayersReviews(),
          finishMatch: this.data.reviewByOrganizer
        };

        this.matchPlayerReviewService.addMatchPlayerReview(request).subscribe({
          next: (response) => {
            this.notificationService.displayNotification(response.result.message, NotificationType.SUCCESS);
            this.closeMatchScoreDialog();
          }
        });
    }
  }

  onRatingChanged($event: any, index: number | null = null): void{
    if(index != null && index != undefined){
      (this.matchScoreForm.get('playersReviews') as UntypedFormArray)
        ?.at(index).get('score')
        ?.setValue($event);
    }
    else{
      this.matchScoreForm.get('matchReviewScore')?.setValue($event);
    }

  }

  onOwnerRatingChanged($event: any) : void{
    this.matchScoreForm.get('matchOwnerReviewScore')?.setValue($event);
  }
  
  dataFetched() : void{
    this.closeMatchScoreDialog();
    this.dataLoading = false;
    this.matchScoreForm.enable();
  }

  closeMatchScoreDialog(): void {
    this.matchScoreDialogReference.close();
  }

  score(group: FormGroup): AbstractControl{
    return group.get('score') as AbstractControl;
  } 

  name(group: FormGroup): AbstractControl{
    return group.get('name') as AbstractControl;
  } 

  private getMatchPlayersReviews() : Array<AddMatchPlayerReviewInfo>{
    const result =  (this.matchScoreForm.get('playersReviews') as UntypedFormArray).controls
      .map(control => {
        const review : AddMatchPlayerReviewInfo = {
          userId: control.get('userId')?.value,
          playerScore: control.get('score')?.value,
          playerScoreDescription: control.get('description')?.value
        };

        return review;
      });
      
    return result;
  }

  private getMatchPlayersForm(players: Array<GetMatchPlayerReviewInfo> | undefined) : FormArray<FormGroup<any>>{
    const controls = this.formBuilder.array<FormGroup>([]);

    this.data.match.matchPlayers.filter(matchPlayer => matchPlayer.userId != this.currentUser.getCurrentUserId())
      .forEach(matchPlayer => 
      {
        const group = this.formBuilder.group({
          name: [matchPlayer.userDisplayName],
          userId: [matchPlayer.userId],
          score : [3],
          description : [''],
        });

        if(players){
          const player = players.find(p => p.matchPlayerId == matchPlayer.matchPlayerId);

          if(player){
            group.controls['score'].setValue(player.playerScore);
            group.controls['description'].setValue(player.playerScoreDescription ?? '');
          }
        }

        controls.push(group)
      }
    );

    return controls;
  }
}


