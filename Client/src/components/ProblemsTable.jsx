import {useState, useEffect} from 'react';
import {Link} from "react-router-dom"


export default function ProblemsTable () {
    const [problems, setProblems] = useState([]);

    useEffect(() => {
        const res = fetch("api/problems", {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            // body: JSON.stringify({}),
        }).then(async (res) => {

            const data = await res.json().catch(() => null);
            setProblems(data);
            console.log(problems)

        })

        
        // setProblems([
        //     {id: 1, title: "Calculate Sum", difficulty: "Easy"},
        //     {id: 2, title: "Check Palindrome", difficulty: "Medium"},
        //     {id: 3, title: "Check repeats in unordered list", difficulty: "Hard"},
        // ]);
    }, []);

    return (
        <div>
        {(problems === null || problems.length === 0) ? ( <div className="table-loading">Loading problems…</div>) : (
        console.log(problems),
        <table className="table">
            <thead>
                <tr>
                <th>Title</th>
                <th>Difficulty</th>
                <th>Status</th>
                <th />
                </tr>
            </thead>

            <tbody>
                {problems.map((p) => (
                    <tr key={p.id}>
                        <td>{p.title}</td>
                        <td>
                        <span className={`badge diff-${p.difficulty.toLowerCase()}`}>
                            {p.difficulty}
                        </span>
                        </td>
                        {/* <td>{solved.has(p.id) ? <span className="badge verdict-accepted">Solved</span> : <span className="muted">—</span>}</td> */}
                        <td>
                            {/* button to take user to a page to solve problem */}
                        <Link className="btn btn-small" to={`/problems/${p.id}`}>
                            Solve
                        </Link>
                        </td>
                    </tr>
                ))}
            </tbody>
        </table>
        )}
        </div>
    )
}