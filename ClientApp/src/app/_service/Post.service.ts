import { comments } from 'src/app/_models/comments';
import { Post } from './../_models/Post';
import { HttpClient, HttpClientModule, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, ɵclearResolutionOfComponentResourcesQueue } from '@angular/core';
import { environment } from 'src/environments/environment';
import { catchError, map, Observable, of } from 'rxjs';
import { PageParams } from '../_models/PagingParams';
import { PaginatedResult } from '../_models/Pagination';

@Injectable({
  providedIn: 'root'
})
export class PostService {

  baseUrl = environment.apiUrl;
  Apikey=environment.Apikey;
  Posts: Post[] = [];
  PostCache = new Map();
  PostsParams: PageParams;
  newPost=false;
  header:any

  constructor(private http: HttpClient) {
  this.PostsParams = new PageParams();
  this.header = this.setHeder();

}



getPostParams() {
return  this.PostsParams;
}

setPostParams(params: PageParams) {
this.PostsParams = params;
}

resetPostsParams(onlyToday=false,onlyComment=false,short=false) {
  this.PostsParams = new PageParams();
  this.PostsParams.onlyToday=(onlyToday ===true)? true:false;
  this.PostsParams.onlyComment=(onlyComment ===true)? true:false;
  this.PostsParams.shortPost=(short ===true)? true:false;
return this.PostsParams;
}
  getPosts(PostsParam: PageParams) {
/*
    var response = this.PostCache.get(Object.values(PostsParam).join('-'));
    if (response && this.newPost===false) {

      return of(response);

    }
    */



    let params = this.getPaginationHeaders(PostsParam.pageNumber, PostsParam.pageSize,PostsParam.onlyToday,PostsParam.onlyComment,PostsParam.shortPost);
    this.newPost=false
    return this.getPaginatedResult<Post[]>(params, this.http)
      .pipe(map(response => {
        if(response.result.length>0)
        {
          this.PostCache.set(Object.values(PostsParam).join('-'), response);
          console.log("chace");
          console.log(response.result);
        }

        return response;
      }))
  }

 getPaginatedResult<PageParams>( params: HttpParams, http: HttpClient) {
    const paginatedResult: PaginatedResult<PageParams> = new PaginatedResult<PageParams>();
console.log(params);

    return http.get<PageParams>(this.baseUrl + 'Post', { observe: 'response', params ,headers: this.header}).pipe(
      map(response => {
        paginatedResult.result = response.body;
        if (response.headers.get('Pagination') !== null) {
          paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
        }
        return paginatedResult;
      })
    );
  }

getPaginationHeaders(pageNumber: number, pageSize: number,onlyToday:boolean,onlyComment:boolean,shortPost:boolean) {
    let params = new HttpParams();

    params = params.append('pageNumber', pageNumber.toString());
    params = params.append('pageSize', pageSize.toString());
    params = params.append('onlyComment', onlyComment.toString());
    params = params.append('onlyToday', onlyToday.toString());
    params = params.append('shortPost', shortPost.toString());

    return params;
  }

onNewPost(){
  this.newPost=true;
}


Add(newPost: Post ){

  return this.http.post<Post>(this.baseUrl +'Post/Add' , newPost, { headers: this.header});
}

setHeder(){
  let header = new HttpHeaders()
  header = header.append('ApiKey', this.Apikey.toString());
  header = header.append( 'Content-Type', 'application/json');
  return header;
}

deletePost (id:number){

  return this.http.delete(this.baseUrl + 'Post/'+ id,{ headers: this.header});

}

addComment(Comment :Comment){

  return this.http.post<comments>(this.baseUrl+'Comment/Add',Comment,{ headers: this.header})
}
}


