import UserData from "./UserData";
export { User, UserData, UserBehavior, Post, Company };

declare global {
  interface UserBehavior {
    user: User;
    profilepic: string;
  }
  class UserData {
    username: string;
    posts: number;
    followers: number;
    following: number;
    profilepic: string;
    constructor(userBehavior: UserBehavior, username: string);
    isCurrentUser(): boolean;
    isCurrentUserFollowing(): boolean;
  }

  interface User {
    _id: string;
    userId: string;
    administrator: boolean;
    company: Company;
    posts: Post[];
    followedUsers: string[];
    followersList: string[];
    likedPost: any[];
    savedPost: number[];
    mentionedPost: any[];
    reportedPost: any[];
  }

  interface Company {
    companyId: number;
    companyName: string;
    bot: any;
    profilePicture: string;
  }

  interface Post {
    postId: number;
    comments: Comment[];
    userId: string;
    username: string;
    content: string;
    dataDiCreazione?: string;
    likes: string[];
    hashtags: string[];
    tags: any[];
  }

  interface Comment {
    commentId: number;
    userId: string;
    username: string;
    content?: string;
    likes: any[];
    replies: Reply[];
  }

  interface Reply {
    replyId: number;
    userId: string;
    username: string;
    content: string;
    likes: any[];
  }
}
