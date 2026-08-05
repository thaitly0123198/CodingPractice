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
                setResult("Pass");
                setErrorMessage("");
                setFailcase("");
            }
            else 
            {
                if (resultRes.compilationErrorMessage)
                    setErrorMessage(resultRes.compilationErrorMessage)
                else
                    setErrorMessage(resultRes.runtimeErrorMessage)
                setResult("Fail");
                setFailcase(resultRes.failedTestcase)
            }
            console.log(resultRes.failedTestcase)
        }
    }, [resultRes]);
    return (
        <div>
             {rs && <pre className={`chip chip--${resultRes.isPass ? 'pass' : 'fail'}`}>{rs}</pre>}
            {errorMessage && <div>{errorMessage}</div>}
            {fc && <div>Failed on input: {fc.input}</div>}
        </div>
    )
}