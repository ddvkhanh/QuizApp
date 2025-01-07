"use client";

import { useRouter } from "next/navigation";
import React, { useEffect, useState } from "react";
import { apiRequest } from "utils/api";

export default function QuestionsPage() {
    const router = useRouter();

    const handleCreateQuestion = () => {
      router.push("/questions/create");
    }

    const goToEditPage = (id: string) => {
      router.push(`/questions/${id}`)
    }

    const handleDelete = async(id: string) => {
      if (!confirm("Are you sure you want to delete this question?")) return;
      
      try {
        const endpoint = `/questions/${id}`;
        const method = "DELETE";      
        await apiRequest(endpoint, method);
        setQuestions(questions.filter((q) => q.id !== id))
      } catch (error: any) {
        console.error(error);
        alert("Failed to delete the question. Please try again.")
      }
    }

    const [questions, setQuestions] = useState<Question[]>([]);

    useEffect(() => {
      fetchQuestions();
    },[])

    const fetchQuestions = async () => {
        try {
            const endpoint = "/questions";
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
        <div className="w-full max-w-3xl p-6 bg-white text-center">
          <div className="mb-6">
          <h1 className="text-2xl font-bold text-center mb-4">Manage Questions</h1>
            <p className="text-center text-gray-600">
            Add, edit, and delete your quiz questions here.
            </p>
          </div>
          <div className="flex justify-center"> 
            <button className="p-3 bg-teal-600 shadow-lg text-white rounded-full hover:bg-teal-700"
              onClick={handleCreateQuestion}>
              Create New Question
            </button>
          </div>
          <div className="pt-7">
            <ul className="space-y-4">
              {questions.map((question) => (
              <li key={question.id} className="p-4 border rounded">
                <p className="font-bold">{question.description}</p>
                <p className="text-sm text-gray-600">Type: {question.questionType === "single" ? "Single Select" : "Multiple Select"}</p>
                <span className="space-x-2">
                  <button className="text-sm underline bg-transparent text-blue-600 hover:bg-blue-300" onClick={() => goToEditPage(question.id)}>Edit</button>
                  <button className="text-sm underline bg-transparent  text-red-600 hover:bg-red-300" onClick={() => handleDelete(question.id)}>Delete</button>
                </span>
                
              </li>
              ))}
            </ul>
          </div>
          
        </div>
      </div>
    );
}
