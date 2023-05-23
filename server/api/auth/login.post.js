import { sendError } from "h3"

export default defineEventHandler(async (event) => {
    const body = await useBody(event)

    const { username, password } = body


        //chiamata alle api

        return{
            body:body
        }

})