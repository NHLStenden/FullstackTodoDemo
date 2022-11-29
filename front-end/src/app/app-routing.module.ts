import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {TodoComponent} from "./todo/todo.component";

const routes: Routes = [
  { path: "todo/:slug", component: TodoComponent},
  { path: "todo", component: TodoComponent},
  { path: "**", redirectTo: 'todo'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
