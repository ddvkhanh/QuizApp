interface Question {
    id: string;
    description: string;
    options: string[];
    correctAnswer: string;
    questionType: "single" | "multiple";
  }