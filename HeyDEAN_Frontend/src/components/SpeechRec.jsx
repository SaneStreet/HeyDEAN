import React from 'react';
import SpeechRecopgnition , { useSpeechRecognition } from 'react-speech-recognition';

const VoiceRec = () => {
    const {
        transcript,
        listening,
        resetTranscript,
        browserSupport
    } = useSpeechRecognition();
}

if (!browserSupport) {
    return (
        <span> Browser does not support speech recognition. </span>
    );
}

return (
    <div>
        <p>{listening ? 'on' : 'off'}</p>
        
    </div>
)