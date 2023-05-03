
export class StringExtendsMethod{

  public static format(target : string, ...replacements: string[]) : string{
        var args = replacements;
        return target.replace(/{(\d+)}/g, function(match, number) {
          return typeof args[number] != 'undefined'
            ? args[number]
            : match
          ;
        });
  }
}
