import {useState, useEffect} from "react";
import { useParams } from "react-router-dom";
import SolutionInputBox from "../components/SolutionInputBox";

export default function() {
    const [problem, setProblem] = useState({});
    const id = useParams().id;
    // console.log(id)
    useEffect(() => {
        const res = fetch(`/nav/problems/${id}`, {method: 'GET', headers: { 'Content-Type': 'application/json' }
        }).then(async (res) => {
            const data = await res.json().catch(() => null);
            setProblem(data);
        })
    }, [id]);

    return(
        <div>
            <div className="problem-description">
            {
                // console.log(problem.examples),
                (problem === null || problem.empty) ? (<div>Loading problem...</div>) : 
                (
                    <div>
                        <h1>{problem.title}</h1>
                        <h3>{problem.category}</h3>
                        <div>{problem.description}</div>
                        <ul>
                            {
                                (problem.examples == null || problem.examples.length == 0) ? (<div>Loading examples...</div>) :
                                problem.examples.map((ex, i)=> (<li key={i}>{ex}</li>))
                            }
                        </ul>
                    </div>
                )
            }
            </div>
            <span>Solve Now!</span>    
            <div>
                <SolutionInputBox/>
            </div>
            
        
        </div>
    )
};