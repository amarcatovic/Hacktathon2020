import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {

  transform(value: any, searchBy: string, prop: string): any {    
    if(!value)
      return;

      if(!searchBy)
      return value;

    if(value.length === 0){
      return value;
    }

    const result = [];
    for(const item of value){
      if(item[prop].toLowerCase().includes(searchBy.toLowerCase())){
        result.push(item);
      }
    }

    return result;
  }
}
