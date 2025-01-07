"use client";

import React from "react";
import QuestionsForm from "components/QuestionsForm";

export default function CreateQuestionPage() {
  return (
    <div className="min-h-screen bg-gray-100 flex items-center justify-center p-4">
      <div className="w-full max-w-3xl p-6 bg-white rounded-lg shadow-md">
        <QuestionsForm />
      </div>
    </div>
  );
}
