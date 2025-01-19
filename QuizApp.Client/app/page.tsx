'use client'

import { useRouter } from "next/navigation";
import { auth0 } from "lib/auth0";

export default function Home() {
  const router = useRouter();
  //const session = auth0.getSession()

  const goToQuiz = () => {
    router.push("/quiz");
  }

  // if (!session) {
  //   return (
  //     <div className="min-h-screen bg-white flex flex-col items-center justify-center">
  //       <div className="w-full max-w-3xl p-10 text-center">
  //         <div className="mb-6">
  //           <h1 className="text-3xl font-bold mb-4">Please log in to use Quiz App</h1>
  //             <a href="/auth/login?screen_hint=signup">Sign up</a>
  //             <a href="/auth/login">Log in</a>
  //         </div>
  //       </div>
  //     </div>
  //   )
  // }

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
