import { useState, useEffect } from "react";

export default function({problemId}) {
    const [solution, setSolution] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [needSubmit, setNeedSubmit] = useState(false);

    async function submitSolution(e, solution) {
        e.preventDefault();
        setIsSubmitting(true);

        // send solution by id to backend
        try {
            const response = await fetch(`/nav/problems/submission/${problemId}`, {
                method: "POST",
                headers: {
                "Content-Type": "application/json", 
                },
                body: JSON.stringify({id: problemId, solution: solution})
            }).then(async (res) => {

                const apires = await res.json().catch(() => null);
                console.log((apires != null || apires != {}) ? apires : "No response from server");

            });
        } catch (error) {
            console.error("Error submitting solution:", error); 
        } finally {
            setIsSubmitting(false);
        }
    }
 
 
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
                <input type="submit" 
                    value={isSubmitting ? "Sending..." : "Submit"} 
                    disabled={isSubmitting || solution.trim() === ""} />
            </form>
        </div>
    )
};