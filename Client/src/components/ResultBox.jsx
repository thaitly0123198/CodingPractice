import { useState, useEffect } from "react";


export default function({resultRes}){
    const [errorMessage, setErrorMessage] = useState("");
    const [rs, setResult] = useState("");
    const [fc, setFailcase] = useState("");
    // console.log(resultRes)

    useEffect(() => {
        if (resultRes)
        {
            if (resultRes.isPass)
            {
                setResult("Pass!");
                setErrorMessage("");
                setFailcase("");
            }
            else 
            {
                if (resultRes.compilationErrorMessage)
                    setErrorMessage(resultRes.compilationErrorMessage)
                else
                    setErrorMessage(resultRes.RuntimeErrorMessage)
                setResult("Fail!");
                setFailcase(resultRes.failedTestcase)
            }
            console.log(resultRes.failedTestcase)
        }
    }, [resultRes]);
    return (
        <div>
             {rs && <h2>{rs}</h2>}
            {errorMessage && <pre>{errorMessage}</pre>}
            {fc && <div>Failed on input: {fc.input}</div>}
        </div>
    )
}