import { OperationError } from "./operation-error.model";

export class OperationResultBase {
  error: OperationError;
  success: boolean;
}
