"use client"

import NavBar from "components/NavBar";
import { useRouter } from "next/navigation";

export default function Home() {
  const router = useRouter();

  const goToQuiz = () => {
    router.push("/quiz");
  }

  return (
    <div className="min-h-screen bg-white flex flex-col items-center justify-center">
      <div className="w-full max-w-3xl p-10 text-center">
        <div className="mb-6">
          <h1 className="text-3xl font-bold mb-4">Welcome to QuizApp</h1>
          <p className="text-lg text-gray-600">Start exploring quizzes now!</p>
          <img
            src="https://c.files.bbci.co.uk/15E07/production/_112970698_qt.jpg"
            alt="Quiz Illustration"
            className="mt-4 w-full max-w-sm mx-auto rounded"
          />
        </div>
        <div className="flex justify-center">
          <button className="animate-pulse p-3 bg-teal-600 shadow-lg text-white rounded-full hover:bg-teal-700"
            onClick={goToQuiz}>
            Take Quiz Now
          </button>
        </div>
      </div>
    </div>
  );
}
