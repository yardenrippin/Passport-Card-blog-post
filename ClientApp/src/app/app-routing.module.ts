import { AddpostComponent } from './BlogPost/addpost/addpost.component';
import { AboutComponent } from './about/about.component';
import { PostComponent } from './BlogPost/post/post.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {path: 'Home', component: HomeComponent},
  {path: '', component: HomeComponent},
  {path: 'Post', component: PostComponent},
  {path: 'about', component: AboutComponent},
  {path: 'addPost', component: AddpostComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
