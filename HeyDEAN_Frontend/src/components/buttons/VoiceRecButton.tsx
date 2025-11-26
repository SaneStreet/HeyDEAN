import { useState } from "react";
import SpeechRecognition, { useSpeechRecognition } from "react-speech-recognition";

export default function VoiceRecButton({ onResult }: { onResult: (text: string) => void }) {
    const [errorMsg, setErrorMsg] = useState("");
    const { 
        transcript,
        resetTranscript,
        listening,
        browserSupportsSpeechRecognition,
        isMicrophoneAvailable
    } = useSpeechRecognition();


    const handleStartListening = () => {
        if (!browserSupportsSpeechRecognition) {
            setErrorMsg("The browser does not support speech recognition.");
            console.log(errorMsg);
            return errorMsg;
        }

        if(!isMicrophoneAvailable)
        {
            setErrorMsg("No Microphone registered.");
            console.log(errorMsg);
            return errorMsg;
        }
        console.log("before startListening")
        SpeechRecognition.startListening({ continuous: false, language: "en-US" });
        console.log(transcript);
    };

    return (
        <button
            className="px-6 py-3 rounded-full bg-blue-600 text-white text-lg shadow hover:bg-blue-700 transition"
            onClick={handleStartListening}
        >
            {listening ? "⏳ Lytter..." : "🎤 Speak to DEAN"}
        </button>
    );
}
