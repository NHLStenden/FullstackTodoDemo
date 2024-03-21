import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable, switchMap} from "rxjs";
import {TodoCategoryResponse} from "../models/TodoCategoryResponse";
import {Category} from "../models/Category";
import {ActivatedRoute, ParamMap} from "@angular/router";
import { TodoService } from './todo.service';

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {

  public todos$: Observable<TodoCategoryResponse[]> | null = null;
  public categories$: Observable<Category[]> | null = null;
  public slug: string | null = "";

  constructor(private todoService: TodoService, private route: ActivatedRoute) {
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      let slug = params.get('slug');
      this.slug = slug;
      if(slug == null) {
        this.todos$ = this.todoService.getTodos();
        // this.todos$ = this._http.get<TodoCategoryResponse[]>("http://localhost:5254/Todo/GetTodosWithCategories");
      } else {
        // this.todos$ = this._http.get<TodoCategoryResponse[]>(`https://localhost:7230/Category/${slug}/todos`);
        this.todos$ = this.todoService.getTodosByCategory(slug);
      }
    });

    this.categories$ = this.todoService.getCategories();
  }
}
