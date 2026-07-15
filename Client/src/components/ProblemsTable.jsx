import {useState, useEffect} from 'react';
import {Link} from "react-router-dom"


export default function ProblemsTable () {
    const [problems, setProblems] = useState([]);
    const [page, setPage] = useState(1);
    const [pageSize, setPagesize] = useState(10);
    const [diffDesc, setSortDifficultyDescending] = useState(false);

    useEffect(() => {
        const res = fetch(`nav/problems?page=${page}&pageSize=${pageSize}&diffDesc=${diffDesc}`, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }).then(async (res) => {
            const data = await res.json().catch(() => null);
            setProblems(data);
            // console.log(data)
        })
    }, [page,  pageSize, diffDesc]);


    // todo: add pagination; sort by difficulty, name; 
    //       filter by difficulty, category(drop down: "Dynamic Programming", "Backtracking", "Bit Manipulation", "String"
    //                                  "Array", "Two Pointers", "Divide and Conquer", "Math","Depth-First Search","Hash Table")
    return (
    <div>
        <div>
            <label>Sort by Difficulty: </label><br/>
            <button onClick={() => {
                setSortDifficultyDescending(d => !d);
                setPage(1);
            }}>
                {diffDesc ? "Hard to Easy" : "Easy to Hard"} 
            </button>
        </div>
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
        <div>
            <button disabled={page==1} onClick={()=>setPage(p => p - 1)}>Previous page</button>
            <button disabled={page * pageSize >= 500} onClick={()=> setPage(p => p + 1)}>Next Page</button>
            <div>Page {page} of {Math.ceil(500 / pageSize)}</div>
            <label>
                Page size: {" "}
                <select 
                    value={pageSize}
                    onChange={(e) => {
                        setPagesize(Number(e.target.value));
                        setPage(1);
                    }}
                >
                    <option value={10}>10</option>
                    <option value={25}>25</option>
                    <option value={50}>50</option>
                    <option value={100}>100</option>
                </select>
            </label>
        </div>
    </div>
    )
}