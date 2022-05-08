import { NavigationEnd, NavigationStart, Router } from '@angular/router';
import { Pagination } from './../../_models/Pagination';
import { Component, ElementRef, EventEmitter, OnInit, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { Post } from 'src/app/_models/Post';
import { PostService } from 'src/app/_service/Post.service';
import { PageParams } from 'src/app/_models/PagingParams';
import { NgxSpinnerService } from 'ngx-spinner';
import { filter } from 'rxjs';
import { CommentComponent } from '../comment/comment.component';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.css']
})
export class PostComponent implements OnInit {
  formModal: any;
  Posts: Post[]=null;
  TempPosts: Post[];
  filters:any[]=[]
  commentT:string[]=[];
  showComments:boolean[]=[];
  pagination: Pagination;
  postParams: PageParams;
  notEmptyPost = true;
  notscrolly = true;
  filterMod:boolean=true

  constructor(private service:PostService,private spinner: NgxSpinnerService ,private router:Router,
    private toastr: ToastrService) {
    this.postParams = this.service.getPostParams();

  }

  ngOnDestroy(): void {
    this.service.resetPostsParams();
    }

  ngOnInit(): void {
    this.setFilters();
    this.loadPosts();

  }

  loadPosts() {
    this.service.setPostParams(this.postParams);
    this.service.getPosts(this.postParams ).subscribe(response => {
    if (response.result.length<1){
      this.service.resetPostsParams();
      this.notEmptyPost = false;
     }
     else{
      this.InitShowComments(response.result.length);
      this.pagination = response.pagination;
      if(this.Posts==null)
      {
        this.Posts=response.result;
      }
      else{
        this.Posts.push(...response.result);
      }
      this.toastr.success("get posts success");
    }
    },error=>{this.toastr.error("canot get post");}
    );

  }

  showComment(index:number){
    this.showComments[index]=!this.showComments[index]
    if( this.showComments[index]==true ){
      this.commentT[index] ="hide comments"
    }else{
      this.commentT[index]="show comments";
    }
  }

  pageScroll() {
    if (this.notEmptyPost)
    {
      this.postParams.pageNumber=this.pagination.currentPage+1;
      this.postParams.pageSize=this.pagination.itemsPerPage
      this.loadPosts();
    }
    else{
      this.toastr.info("no mor posts to show");
    }

     }

deletePost(id:number,index:number){
  this.service.deletePost(id).subscribe(response=>{
    this.Posts.splice(index,1);
    this.service.onNewPost();
    this.service.resetPostsParams();
    this.toastr.success("post delete success")
    if(this.Posts.length<5) {
      this.loadPosts();
    }


  },error => {this.toastr.error("error on delete")})

  }

  InitShowComments(Elements:number)
  {
   for (let index = 0; index < Elements; index++) {
     this.showComments.push(false);
     this.commentT.push("show comments");
   }
  }
  setFilters(){
    this.filters.push({id:1,status:false,name:"Only comments"});
    this.filters.push({id:2,status:false,name:"Only Today"});
    this.filters.push({id:3,status:false,name:"Short Posts"});
    }

    filterAction(){
    this.service.resetPostsParams();
    this.postParams=this.service.getPostParams();
    this.setFilterToPaging();
    this.Posts=null;
    this.service.onNewPost();
    this.loadPosts()

    }

    setFilterToPaging(){

      for (let index = 0; index < this.filters.length; index++) {

        switch (this.filters[index].id) {
          case 1:
            this.postParams.onlyComment=this.filters[index].status;
           break;
          case 2:
            this.postParams.onlyToday=this.filters[index].status;
           break;
          case 3:
            this.postParams.shortPost=this.filters[index].status;
            break;

          default:
            break;
        }

      }
    }
}
