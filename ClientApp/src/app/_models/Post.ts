import { Comment } from './comment';
import { comments } from './comments';

export interface Post {
  id:number;
  author:string;
  title:string;
  content:string;
  dateTime:Date;
  comments:comments[]


}
