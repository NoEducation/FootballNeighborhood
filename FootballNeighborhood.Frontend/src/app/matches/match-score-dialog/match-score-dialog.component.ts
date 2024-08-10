import { Component, Inject, OnInit } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormGroup, UntypedFormArray, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { StarRatingColor } from 'src/app/common/star-rating/star-rating.component';
import { Match } from 'src/app/models/matches/match.model';
import { CurrentUserService } from 'src/app/services/current-user.service';
import { AddMatchPlayerReviewRequest } from '../models/add-match-player-review-request.model';
import { AddMatchPlayerReview } from '../models/add-match-player-review.model';
import { MatchPlayerReviewService } from 'src/app/services/match-player-review-service';
import { NotificationService } from 'src/app/services/communication/notification.service';
import { NotificationType } from 'src/app/models/common/notification-type.constraint';
import { PlayerMatchStatusEnum } from 'src/app/models/matches/player-match-status-enum';

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
    @Inject(MAT_DIALOG_DATA) public readonly data: Match,
    private readonly formBuilder: FormBuilder,
    private readonly currentUser: CurrentUserService,
    private readonly matchPlayerReviewService: MatchPlayerReviewService,
    private readonly notificationService: NotificationService,
    public readonly matchScoreDialogReference: MatDialogRef<MatchScoreDialogComponent>) { }

  ngOnInit() {
    this.matchScoreForm = this.formBuilder.group({
      matchReviewScore : [this.rating, [Validators.required]],
      matchReviewDescription : [],
      playersReviews : this.getMatchPlayersForm()
    });

    if(this.data.playerMatchStatus == PlayerMatchStatusEnum.Reviewed){
      this.matchScoreForm.disable();
      this.isReadonly = true;
    }
  }

  onSubmit(): void {
    if (this.matchScoreForm.valid) {
        this.dataLoading = true;
        this.matchScoreForm.disable();

        const request : AddMatchPlayerReviewRequest = {
          matchId: this.data.matchId,
          matchReviewScore: this.matchScoreForm.get('matchReviewScore')?.value,
          matchReviewDescription: this.matchScoreForm.get('matchReviewDescription')?.value,
          playerReviews: this.getMatchPlayersReviews()
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
      this.rating = $event;
    }

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

  private getMatchPlayersReviews() : Array<AddMatchPlayerReview>{
    const result =  (this.matchScoreForm.get('playersReviews') as UntypedFormArray).controls
      .map(control => {
        const review : AddMatchPlayerReview = {
          playerId: control.get('userId')?.value,
          playerScore: control.get('score')?.value,
          playerScoreDescription: control.get('description')?.value
        };

        return review;
      });
      
    return result;
  }

  private getMatchPlayersForm() : FormArray<FormGroup<any>>{
    const controls = this.formBuilder.array<FormGroup>([]);

    this.data.matchPlayers.filter(matchPlayer => matchPlayer.userId != this.currentUser.getCurrentUserId())
      .forEach(matchPlayer => 
      {
        const group = this.formBuilder.group({
          name: [matchPlayer.userDisplayName],
          userId: [matchPlayer.userId],
          score : [3],
          description : [''],
        });
      
        controls.push(group)
      }
    );

    return controls;
  }
}


