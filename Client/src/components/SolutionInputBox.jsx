import { useState } from "react";

function submitSolution(e, solution) {
    e.preventDefault();

    // todo: send solution by id to backend
    const response = await fetch(`/submission/${id}`, {
        method: "POST",
        headers: {
          // Tell the backend we are sending JSON data
          "Content-Type": "application/json", 
        },
        // 4. Convert our React state into a JSON object
        body: JSON.stringify({ 
          solution: solutionText 
        })
    });

}

export default function() {
    const [solution, setSolution] = useState("");

    return (
        <div>
            <form onSubmit={(e) => submitSolution(e, solution)}>
                <label htmlFor={`solution-submission`}>Solution:</label>
                <div></div>
                <textarea 
                    placeholder="Paste your solution here..." 
                    rows={10} cols={50}
                    id={`solution-submission`}
                    onChange={(e) => setSolution(e.target.value)}
                >   
                </textarea>
                <div></div>
                <input type="submit" value="Submit" />
            </form>
        </div>
    )
};