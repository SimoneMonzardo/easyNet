export default defineEventHandler(async (event)=>{
    
    //get posts with api

    //fake implementation
    const posts = { 
        PostId : 1,
        Comment : [
        {
            CommentId : 1,
            UserId : 3,
            Username : "Giovanni",
            Content : "esempio di commento",
            Like : [2,3,5,4],
            Replies : []

        },
        {
            CommentId : 2,
            UserId : 5,
            Username : "Giorgio",
            Content : "altro esempio di commento",
            Like : [4,6,3,1],
            Replies : [
                {
                CommentId : 2,
                UserId : 4,
                Username : "Carlo",
                Content : "esempio di reply",
                Like : [12,15,5,3,1]
                },
                {
                    CommentId : 4,
                    UserId : 5,
                    Username : "Pino",
                    Content : "altro esempio di reply",
                    Like : [2,5,43,4]
                }]
        }
        ],
        UserId : 1,
        username: "Marione",
        content : "questo è un esempio di contenuto di un post",
        likes : [2,54,12,4,3,5,1],
        Hastags : ["#abc","#cde","#efg"],
        Tags : [2,14,15,43,12,42]
    }

    return  {
        posts:posts
    }

})