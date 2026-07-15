import {useState, useEffect} from "react";
import { useParams, useLocation } from "react-router-dom";
import SolutionInputBox from "../components/SolutionInputBox";
import ResultBox from "../components/ResultBox";

export default function() {
    const pid = useLocation().state?.id || useParams().id;
    const [problem, setProblem] = useState({});
    const [pl, setPlaceholder] = useState("");
    const [result, setResult] = useState();
    const [fName, setFuncName] = useState("");
    
    //console.log(pid)
    useEffect(() => {
        const res = fetch(`/nav/problems/${pid}`, {method: 'GET', headers: { 'Content-Type': 'application/json' }
        }).then(async (res) => {
            const data = await res.json().catch(() => null);
            // console.log(data)
            setPlaceholder(data.solutionBoxPlaceholder);
            setFuncName(data.functionName);
            setProblem(data);
        })
    }, []);

    function submitSolution(e, solution) {
        e.preventDefault();
        // console.log(problem.solution)
        // const fName = problem.solution.FunctionName;

        // send solution by id to backend
        const response = fetch(`/nav/problems/submission/${pid}`, {
            method: "POST",
            headers: {
            "Content-Type": "application/json", 
            },
            body: JSON.stringify({FunctionName: fName, solution: solution})
        }).then(async (res) => {

            const apires = await res.json().catch(() => null);
            // console.log((apires != null || apires != {}) ? apires : "No response from server");
            setResult(apires);
        });
        
    }    

    // todo: use Code Mirror 6 to replace the inputbox -> solve the python indentation problem
    return(
        <div>
            <div className="problem-description">
            {
                // console.log(problem),
                (problem === null || problem.empty) ? (<div>Loading problem...</div>) : 
                (
                    <div>
                        <h1>{problem.title}</h1>
                        <h3>{`Category: ${problem.category}`}</h3>
                        <div>{problem.description}</div><br />
                        <div style={{ fontWeight: 'bold' }}>{`Constraint(s): ${problem.constraint}`}</div><br />
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
            <div>
                <SolutionInputBox submitHandler={submitSolution} placeholder={pl} />
            </div>
            <div>
                <ResultBox resultRes={result}/>
            </div>
        </div>
    )
};