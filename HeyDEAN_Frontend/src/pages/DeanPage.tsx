import { useState } from "react";
import VoiceRecButton from "../components/buttons/VoiceRecButton";
import MultiPanel from "../components/panels/MultiPanel";
import ChatBubbleUser from "../components/ChatBubbleUser";
import ChatBubbleDean from "../components/ChatBubbleDean";

export default function DeanPage() {
    const [messages, setMessages] = useState<{ from: "user" | "dean"; text: string; panel?: any }[]>([]);
    const [error, setError] = useState("");

    const handleVoiceInput = async (userText: string) => {
        const token = localStorage.getItem("token");

        setMessages(prev => [...prev, { from: "user", text: userText }]);

        try {
            const res = await fetch("http://localhost:5152/api/dean/ask", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                    Authorization: `Bearer ${token}`,
                },
                body: JSON.stringify({ prompt: userText }),
            });

            const data = await res.json();
            console.log(data);

            if (data.type === "notes" || data.type === "tasks" || data.type === "events") {
                setMessages(prev => [...prev, { from: "dean", text: data.message, panel: data.items }]);
            } else {
                setMessages(prev => [...prev, { from: "dean", text: data.message }]);
            }

        } catch {
            setMessages(prev => [...prev, { from: "dean", text: "Error connecting to backend." }]);
            setError("Error connecting to backend.");
            console.log(error);
        }


    };

    return (
        <div className="flex flex-col items-center p-6 space-y-6 w-full">
            <h1 className="text-3xl font-bold">Greetings 👋</h1>
            <p className="text-lg opacity-70">What can I help you with?</p>
            {error && <p className="text-red-500 mt-2.5">{error}</p>}

            <VoiceRecButton onResult={handleVoiceInput} />

            {error && <p className="text-red-500 mt-2.5">{error}</p>}
            <div className="w-full min-h-fit max-w-lg space-y-4 mt-6">
                {messages.map((msg, idx) => (
                    msg.from === "user" ? (
                        <ChatBubbleUser key={idx} text={msg.text} />
                    ) : (
                        <>
                            <ChatBubbleDean key={`dean-${idx}`} text={msg.text} />
                            {msg.panel && msg.panel.length > 0 && <MultiPanel text={msg.text} items={msg.panel} />}
                        </>
                    )
                ))}
                {error && <p className="text-red-500 mt-2.5">{error}</p>}
            </div>
            {error && <p className="text-red-500 mt-2.5">{error}</p>}
        </div>
    );

}