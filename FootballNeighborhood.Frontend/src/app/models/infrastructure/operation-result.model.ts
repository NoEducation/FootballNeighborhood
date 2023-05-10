import { OperationResultBase } from "./operation-result-base.model";

export class OperationResult<T> extends OperationResultBase{
   result : T;
}
