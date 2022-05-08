import { Post } from './../../_models/Post';
import { PostService } from './../../_service/Post.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-addpost',
  templateUrl: './addpost.component.html',
  styleUrls: ['./addpost.component.css']
})
export class AddpostComponent implements OnInit {

  Post: any={}
  constructor(private router: Router, private service:PostService,private toastr: ToastrService) { }

  ngOnInit(): void {

  }

AddPost(newPost: any) {

 this.service.Add(newPost).subscribe(()=>{
  this.Post={};
  this.service.onNewPost();
  this.service.resetPostsParams();
  this.toastr.success("New post is coming");
  this.router.navigate(['/Post']);

},error => {this.toastr.error("fail to add a post");});

}

}
