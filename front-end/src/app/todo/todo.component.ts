import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable, switchMap} from "rxjs";
import {TodoCategoryResponse} from "../models/TodoCategoryResponse";
import {Category} from "../models/Category";
import {ActivatedRoute, ParamMap} from "@angular/router";

@Component({
  selector: 'app-todo',
  templateUrl: './todo.component.html',
  styleUrls: ['./todo.component.css']
})
export class TodoComponent implements OnInit {
  private _http: HttpClient;

  //public data: object | null = null;
  public todos$: Observable<TodoCategoryResponse[]> | null = null;
  public categories$: Observable<Category[]> | null = null;
  public slug: string | null = "";
  private _route$: ActivatedRoute;

  constructor(http: HttpClient, private route: ActivatedRoute) {
    this._http = http;
    this._route$ = route;
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe((params: ParamMap) => {
      let slug = params.get('slug');
      this.slug = slug;
      if(slug == null) {
        this.todos$ = this._http.get<TodoCategoryResponse[]>("http://localhost:5254/Todo/GetTodosWithCategories");
      } else {
        this.todos$ = this._http.get<TodoCategoryResponse[]>(`https://localhost:7230/Category/${slug}/todos`);
      }
    });

    this.categories$ = this._http.get<Category[]>("https://localhost:7230/Category/Get");
  }
}
