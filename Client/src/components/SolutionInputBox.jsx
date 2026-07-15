import { useState, useEffect } from "react";

export default function({placeholder, submitHandler}) {
    const [solution, setSolution] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    useEffect(() => {
        setSolution(placeholder ?? "");
    }, [placeholder]);

    function handleKeyDown(e) {
        if (e.key !== "Tab") return;
        e.preventDefault();
        const el = e.target;
        const start = el.selectionStart, end = el.selectionEnd;
        const indent = "    "; // 4 spaces, never \t
        const next = solution.slice(0, start) + indent + solution.slice(end);
        setSolution(next);
        // controlled input: react re-render resets the caret n put it back after paint
        requestAnimationFrame(() => { el.selectionStart = el.selectionEnd = start + indent.length; });
    }
    
    return (
        <div>
            <form onSubmit={(e) => submitHandler(e, solution)}>
                <label htmlFor={`solution-submission`}>Your Python Solution (Indentation = 4 spaces):</label>
                <div></div>
                <textarea 
                    // placeholder={placeholder ?? "Paste your solution here.."}
                    rows={30} cols={100}
                    id={`solution-submission`}
                    onChange={(e) => setSolution(e.target.value)}
                    value={solution}
                    onKeyDown={handleKeyDown}
                >   
                </textarea>
                <div></div>
                <input type="submit" 
                    value={isSubmitting ? "Sending..." : "Submit"} 
                />
            </form>
        </div>
    )
};