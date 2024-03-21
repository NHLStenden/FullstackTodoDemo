import { Component } from '@angular/core';
import { TodoService } from '../todo.service';
import {BehaviorSubject, Observable, combineLatest, debounceTime, filter, switchMap, tap } from 'rxjs';
import { TodoCategoryResponse } from 'src/app/models/TodoCategoryResponse';

@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.css']
})
export class SearchComponent {

  search$ = new BehaviorSubject<string>('');
  todos$: Observable<TodoCategoryResponse[]> | null = null;

  selectedItems$: BehaviorSubject<TodoCategoryResponse | null> =
    new BehaviorSubject<TodoCategoryResponse | null>(null);
  delete$ : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
  areYouSure$ : BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private todoService: TodoService) {
  }

  public search(event: any) {
    let search = event.target.value;

    this.search$.next(search);

    this.todos$ = this.search$.pipe(
      debounceTime(2000),
      switchMap(x => this.todoService.searchTodo(x))
    );

    combineLatest([this.selectedItems$, this.delete$, this.areYouSure$])
      .pipe(filter(([selectedItems, delete$, areYouSure$]) =>
        selectedItems != null && delete$ && areYouSure$)).subscribe(
          _ => console.log('Deleting')
    );


    // console.log(search);
    // if(search != null) {
    //   // this.todos$ = this.todoService.searchTodo(search);
    // }
  }

  select(selectedItem: TodoCategoryResponse) {
    this.selectedItems$.next(selectedItem);
  }

  delete() {
    this.delete$.next(true);
  }

  areYouSure() {
    this.areYouSure$.next(true);
  }
}
