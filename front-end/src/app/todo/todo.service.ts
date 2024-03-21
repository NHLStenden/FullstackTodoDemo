import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TodoCategoryResponse } from '../models/TodoCategoryResponse';
import { Category } from '../models/Category';

@Injectable({
  providedIn: 'root'
})
export class TodoService {

  private _http: HttpClient;

  constructor(http: HttpClient) {
    this._http = http;
  }

  public getTodos() {
    return this._http.get<TodoCategoryResponse[]>("http://localhost:5254/Todo/GetTodosWithCategories");
  }

  public getTodosByCategory(slug: string) {
    return this._http.get<TodoCategoryResponse[]>(`https://localhost:7230/Category/${slug}/todos`);
  }

  public getCategories() {
    return this._http.get<Category[]>("https://localhost:7230/Category/Get");
  }

  public searchTodo(search: string) {
    return this._http.get<TodoCategoryResponse[]>(`https://localhost:7230/Todo/Search?description=${search}`);
  }
}
