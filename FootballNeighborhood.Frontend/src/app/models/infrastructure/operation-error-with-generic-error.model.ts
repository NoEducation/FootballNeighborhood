import { Guid } from "guid-typescript";
import { OperationResultBase } from "./operation-result-base.model";

export class OperationResultWithGenericError extends OperationResultBase {
  displayGenericException : boolean;
  unexpectedErrorOccurred : boolean;
  code : Guid;
}
