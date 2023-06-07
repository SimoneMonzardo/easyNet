export default class UserData {
  username: string;
  posts: number;
  followers: number;
  following: number;
  profilepic: string;
  constructor(userBehavior: UserBehavior, username: string) {
    this.username = username;
    this.posts = userBehavior.user.posts.length;
    this.followers = userBehavior.user.followersList.length;
    this.following = userBehavior.user.followedUsers.length;
    this.profilepic = userBehavior.profilepic;
  }
  isCurrentUser() {
    const currentUserName = sessionStorage.getItem("username");
    return this.username === currentUserName;
  }
  isCurrentUserFollowing(user: User) {
    const currentUserName = sessionStorage.getItem("username");
    if (currentUserName === null) {
      return false;
    }
    return user.followersList.includes(currentUserName);
  }
}
