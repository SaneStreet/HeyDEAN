import { useState, useEffect, useRef } from "react";

// Type declarations for Web Speech API
declare global {
    interface Window {
        SpeechRecognition: any;
        webkitSpeechRecognition: any;
    }
}

export default function VoiceRecButton({ onResult }: { onResult: (text: string) => void }) {
    const [errorMsg, setErrorMsg] = useState("");
    const [listening, setListening] = useState(false);
    const recognitionRef = useRef<any>(null);

    useEffect(() => {
        // Check browser support
        const SpeechRecognition = window.SpeechRecognition || (window as any).webkitSpeechRecognition;
        
        if (!SpeechRecognition) {
            setErrorMsg("The browser does not support speech recognition.");
            return;
        }

        // Initialize recognition
        const recognition = new SpeechRecognition();
        recognition.continuous = false;
        recognition.interimResults = false;
        recognition.lang = "en-US";

        recognition.onstart = () => {
            setListening(true);
            setErrorMsg("");
        };

        recognition.onresult = (event: any) => {
            const transcript = event.results[0][0].transcript;
            onResult(transcript);
            setListening(false);
        };

        recognition.onerror = (event: any) => {
            console.error("Speech recognition error:", event.error);
            setErrorMsg(`Error: ${event.error}`);
            setListening(false);
        };

        recognition.onend = () => {
            setListening(false);
        };

        recognitionRef.current = recognition;

        return () => {
            if (recognitionRef.current) {
                recognitionRef.current.stop();
            }
        };
    }, [onResult]);

    const handleStartListening = () => {
        if (errorMsg) {
            console.log(errorMsg);
            return;
        }

        if (recognitionRef.current && !listening) {
            recognitionRef.current.start();
        }
    };

    return (
  <button
    className="relative px-8 py-4 rounded-full border-2 border-green-400/50 bg-black/50 backdrop-blur-md text-green-400 font-mono text-lg shadow-2xl hover:shadow-green-400/25 hover:border-green-400 hover:bg-green-400/10 transition-all duration-300 hover:scale-105 disabled:opacity-50 disabled:cursor-not-allowed"
    onClick={handleStartListening}
    disabled={listening}
  >
    <div className="absolute inset-0 rounded-full bg-gradient-to-r from-green-400/20 to-cyan-400/20 blur-lg"></div>
    <div className="relative flex items-center space-x-2">
      {listening ? (
        <>
          <div className="w-3 h-3 bg-green-400 rounded-full animate-pulse"></div>
          <span className="text-cyan-400">LISTENING...</span>
        </>
      ) : (
        <>
          <span className="text-xl">🎤</span>
          <span>SPEAK TO DEAN</span>
        </>
      )}
    </div>
  </button>
);
}
