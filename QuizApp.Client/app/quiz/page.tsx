"use client";

import QuizCard from "components/QuizCard";
import { useRouter } from "next/navigation";
import React, { useEffect, useState } from "react";
import { apiRequest } from "utils/api";

export default function QuizPage() {
  const router = useRouter();

  const [questions, setQuestions] = useState<Question[]>([]);
  const [isQuizStart, setQuizStart] = useState(false);

  useEffect(() => {
    fetchQuestions();
  }, [])

  const toggleQuizMode = () => {
    setQuizStart(true);
  }

  const fetchQuestions = async () => {
    try {
      const endpoint = "/questions?random=true";  //called the backend fetch questions randomly
      const method = "GET";
      const response: Question[] = await apiRequest<Question[]>(endpoint, method);
      setQuestions(response);
    } catch (error) {
      console.error("Error:", error);
      alert("Failed to fetch the questions. Please try again.");
    }
  }

  return (
    <div className="min-h-screen flex flex-col items-center justify-center">
      <div className="w-full max-w-3xl p-10 bg-slate-800 shadow-lg rounded-lg text-white text-center">
      {!isQuizStart ? (
        <div className="">
          <div className="mb-6">
            <h1 className="text-2xl font-bold text-center mb-4">Quiz Rules</h1>
            <ul className="list-decimal">
              <li className="mb-2">Ensure you read each question and all options thoroughly before answering.</li>
              <li className="mb-2">Once submitted, your answers cannot be changed or resubmitted.</li>
              <li className="mb-2">Your score will be calculated based on the number of correct answers.</li>
              <li className="mb-2">Submit the quiz only when you have reviewed all your answers.</li>
            </ul>
          </div>
          <div className="flex justify-center">
            
              <button className="p-3 bg-teal-600 shadow-lg text-white rounded-full hover:bg-teal-700"
                onClick={toggleQuizMode}>
                Start Quiz
              </button>
          </div>
        </div>
        ) :
            (<QuizCard questions={questions} />)}
        </div>
    </div>
  );
}
