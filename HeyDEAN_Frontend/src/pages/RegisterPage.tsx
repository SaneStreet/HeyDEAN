import { useState } from "react";
import { useNavigate } from "react-router-dom";

export default function RegisterPager () {
    const [error, setError] = useState("");
    const navigate = useNavigate();

    return (
        <div>
            <h2>This is Register page</h2>
            <p>Insert Form for registration later.</p>
        </div>
    );
}