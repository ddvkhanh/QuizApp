
"use client"

import { useSearchParams } from "next/navigation";

export default function ScorePage() {
    const searchParams = useSearchParams();
    const score = searchParams.get("score");

    return (
        <div className="min-h-screen flex flex-col items-center justify-center">
            <div className="w-full max-w-3xl p-6 bg-white text-center">
                <div className="mb-6">
                    <h1 className="text-xl font-bold text-center mb-4">Quiz Result</h1>
                    <p className="text-3xl text-center text-gray-600 mb-6">
                        Your Score is: {score}
                    </p>
                </div>
            </div>
        </div>
    )
}