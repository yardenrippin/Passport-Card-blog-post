import { Component, Input, OnInit } from '@angular/core';
import { comments } from 'src/app/_models/comments';
import { Post } from 'src/app/_models/Post';
import { PostService } from 'src/app/_service/Post.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-comment',
  templateUrl: './comment.component.html',
  styleUrls: ['./comment.component.css']
})
export class CommentComponent implements OnInit {
  @Input() comments: comments[];
  @Input() PostId: number
  commentToAdd:any={}

 commentStr:string=""
  constructor(private service:PostService, private toastr: ToastrService ) { }

  ngOnInit(): void {


  }
  addComment(){
    if(this.commentStr.length>2){

      this.commentToAdd={content:this.commentStr,PostId:this.PostId}
      this.service.addComment(this.commentToAdd).subscribe(response=>{
       this.comments.push(response);
       this.service.onNewPost();
       this.commentStr="";
       this.toastr.success("comment add success");
      },error => {this.toastr.error("error on save comment");}

      );

    }
    else
    {
      alert("comment canot be null ");
    }
  }
}

