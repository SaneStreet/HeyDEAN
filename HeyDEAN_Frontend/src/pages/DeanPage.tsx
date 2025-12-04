import { useState } from "react";
import VoiceRecButton from "../components/buttons/VoiceRecButton";
import MultiPanel from "../components/panels/MultiPanel";
import { useAuth } from "../context/AuthContext";
import { useNavigate } from "react-router-dom";

export default function DeanPage() {
    const [error, setError] = useState("");
    const { username, logout } = useAuth();
    const navigate = useNavigate();
    const [panelState, setPanelState] = useState<{
        type: 'notes' | 'tasks' | 'events' | null;
        data: any[];
        }>({ type: null, data: [] });
    

    //console.log("DeanPage - token:", token);
    //console.log("DeanPage - username:", username);
    //console.log("DeanPage - typeof username:", typeof username);

    const greetings = [
        "Howdy",
        "Greetings",
        "Hello there",
        "Welcome",
        "Salutations",
    ];

    const getRandomGreeting = () => {
        return greetings[Math.floor(Math.random() * greetings.length)]
    };

    const handleVoiceInput = async (userText: string) => {
        const token = localStorage.getItem("token");
        const lowerUserText = userText.toLowerCase();
        console.log("User:\n " + lowerUserText)

        console.log("Checking for voice commands that shows appropriate data")
        try {
            let API_URL = "http://localhost:5152/api/"
            if (lowerUserText.includes("note")) {
                const result = await fetch(API_URL + "notes", {
                    headers: { Authorization: `Bearer ${token}`}
                });
                console.log("Notes fetched from API.");
                const notesData = await result.json();
                setPanelState({ type: "notes", data: notesData})
            } 
            else if (lowerUserText.includes("task")) {
                const result = await fetch(API_URL + "tasks", {
                    headers: { Authorization: `Bearer ${token}` }
                });
                console.log("Tasks fetched from API.");
                const tasksData = await result.json();
                setPanelState({type: "tasks", data: tasksData});
            }
            else if (lowerUserText.includes("event")) {
                const result = await fetch(API_URL + "events", {
                    headers: { Authorization: `Bearer ${token}`}
                });
                console.log("Events fetched from API.");
                const eventsData = await result.json();
                setPanelState({type: "events", data: eventsData})
            }
            else {
                const result = await fetch(API_URL + "dean/ask", {
                    method: "POST",
                    headers: { 
                        Authorization: `Bearer ${token}`,
                        "Content-Type": "application/json",
                    },
                    body: JSON.stringify({ prompt: userText}),
                });
                console.log("Fallback to DEAN. No command was executed.");
                navigate("/dean");
                await result.json();
            }

        } catch (error){
            setError("Error connecting to backend.");
            console.log("Error connecting to backend.\n" + error);
        }
    };

    {/* Handles onclick event for logging out a user. This clears cookies for all data stored. */}
    const handleLogout = () => {
        logout();
        navigate("/login");
    }

    return (
        <div className="min-h-screen relative">
            <div className="matrix-bg"></div>
            <div className="relative z-10 flex flex-col items-center p-6 space-y-6 w-full">
                <div className="text-center">
                    <h1 className="text-4xl font-bold text-transparent bg-clip-text bg-linear-to-r from-green-400 to-cyan-400 mb-2">
                        {getRandomGreeting()}, {username && `${username}`}
                    </h1>
                    <div className="h-px w-32 bg-linear-to-r from-transparent via-green-400 to-transparent mx-auto mb-4"></div>
                    <p className="text-lg text-green-300 opacity-80 font-mono">
                        &gt; What can I help you with?
                    </p>
                </div>

                {/* VoiceRecButton */}
                <div className="relative">
                    <div className="absolute inset-0 bg-green-400 blur-xl opacity-20 animate-pulse"></div>
                    <VoiceRecButton onResult={handleVoiceInput} />
                </div>
                {/* MultiPanel for showing notes, tasks, and events */}
                <div className="flex min-h-64 min-w-2xl p-2.5 backdrop-blur-md">
                {panelState.type && (
                    <MultiPanel
                        type={panelState.type}
                        data={panelState.data}
                        onItemAction={(item, action) => {
                        // Handle item actions like toggle task completion
                        }}
                    />
                )}
                    {/* Error messages */}
                    {error && (
                        <div className="max-w-md p-4 border border-red-500/50 rounded-lg bg-red-950/50 backdrop-blur-sm">
                        <p className="text-red-400 font-mono text-sm">
                            [ERROR] {error}
                        </p>
                        </div>
                    )}
                </div>
                <button onClick={handleLogout}>Logout</button>
            </div>
        </div>
    );

}
