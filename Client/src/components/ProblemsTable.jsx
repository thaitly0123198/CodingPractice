import {useState, useEffect} from 'react';
import {Link} from "react-router-dom"


export default function ProblemsTable () {
    const [problems, setProblems] = useState([]);

    useEffect(() => {
        const res = fetch(`nav/problems`, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).then(async (res) => {
            const data = await res.json().catch(() => null);
            setProblems(data);
            // console.log(data)
        })
    }, []);

    return (
        <div>
        {
            (problems === null || problems.length === 0) ? ( <div className="table-loading">Loading problems…</div>) : 
            (
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
                                <td><span>{p.difficulty}</span></td>
                                {/* add per user mutations */}
                                {/* <td>{solved.has(p.id) ? <span className="solved">Solved</span> : <span className="muted">—</span>}</td> */}
                                <td>
                                    {/* button to take user to a page to solve problem */}
                                <Link className="btn btn-small" to={`${p.id}`} state={{ id: p.id }}>
                                    Solve
                                </Link>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            )
        }
        </div>
    )
}