export default defineEventHandler(async (event)=>{
    
    //get post with api

    const posts = { 
        username: "Marione",
        content : "questo è un esempio di contenuto di un post",
        likes : 123
    }

    return  {
        posts:posts
    }

})