"use client";

import React, { useState, useEffect } from "react";
import { Formik, Form, FieldArray, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import { useRouter } from "next/navigation";
import { apiRequest } from "utils/api";

export default function QuestionsForm({ id }: { id?: string }) {
  const router = useRouter();
  const isEditMode = !!id;

  const [loading, setLoading] = useState(false);
  
  const [initialValues, setInitialValues] = useState({
    questionType: "single",
    description: "",
    options: ["Option 1", "Option 2"],
    correctAnswer: "",
  })

  useEffect(() => {
    if (isEditMode) {
      const fetchQuestions = async() => {
        try {
          const response:Question = await apiRequest<Question>(`/questions/${id}`, "GET")
          setInitialValues({
            questionType: response.questionType,
            description: response.description,
            options: response.options,
            correctAnswer: response.correctAnswer,
          })
        } catch (error:any) {
          console.error(error);
          alert("Failed to fetch the question. Please try again.");
        }
      };
      fetchQuestions();
    }

  },[id, isEditMode])

  const validationSchema = Yup.object().shape({
    questionType: Yup.string().required("Question type is required"),
    description: Yup.string().required("Question description is required"),
    options: Yup.array()
      .of(Yup.string().required("Option cannot be empty"))
      .min(2, "At least two options are required")
      .test(
        "unique-options",
        "Options must be unique",
        (options) => new Set(options).size === options.length
      ),
    correctAnswer: Yup.string().required("At least one correct answer is required"),
  });

  const handleSubmit = async (values: typeof initialValues) => {
    setLoading(true);
    const payload = { 
      id: isEditMode ? id: undefined,
      ...values };

    try {
      const endpoint = `/questions${isEditMode ? `/${id}` : ""}`;
      const method = isEditMode ? "PUT" : "POST";
      console.log(payload);
      const response = await apiRequest(endpoint, method, payload);

      if (response) {
        alert(isEditMode ? "Question updated successfully!" : "Question created successfully!");
        router.push("/questions");
      } else {
        throw new Error("Failed to save the question");
      }
    } catch (error) {
      console.error("Error:", error);
      alert("Failed to save the question. Please try again.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="container mx-auto p-8">
      <h1 className="text-3xl font-bold text-center mb-6">
        {isEditMode ? "Edit Question" : "Add New Question"}
      </h1>
      <div className="bg-white p-6 rounded-lg shadow-md">
        <Formik
          initialValues={initialValues}
          validationSchema={validationSchema}
          onSubmit={handleSubmit}
          enableReinitialize
        >
          {({ values, setFieldValue }) => (
            <Form>
              {/* Question Type */}
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700">Question Type</label>
                <Field as="select" name="questionType" className="w-full p-2 mt-1 border rounded">
                  <option value="single">Single Choice</option>
                  <option value="multiple">Multiple Choice</option>
                </Field>
                <ErrorMessage name="questionType" component="div" className="text-red-500 text-sm" />
              </div>

              {/* Question */}
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700">Question</label>
                <Field
                  name="description"
                  as="textarea"
                  placeholder="Enter your question description"
                  className="w-full p-2 mt-1 border rounded"
                />
                <ErrorMessage name="description" component="div" className="text-red-500 text-sm" />
              </div>

              {/* Options */}
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700">Options</label>
                <FieldArray name="options">
                  {({ push, remove }) => (
                    <div>
                      {values.options.map((option, index) => (
                        <div key={index} className="flex items-center gap-2 mb-2">
                          <Field
                            name={`options[${index}]`}
                            placeholder={`Option ${index + 1}`}
                            className="w-full p-2 border rounded"
                          />
                          <button
                            type="button"
                            className="px-3 py-1 text-white bg-red-500 rounded hover:bg-red-600"
                            onClick={() => remove(index)}
                          >
                            Remove
                          </button>
                        </div>
                      ))}
                      <button
                        type="button"
                        className="px-3 py-1 text-white bg-teal-500 rounded hover:bg-teal-600"
                        onClick={() => push("")}
                      >
                        Add Option
                      </button>
                    </div>
                  )}
                </FieldArray>
                <ErrorMessage name="options" component="div" className="text-red-500 text-sm" />
              </div>

              {/* Correct Answer(s) */}
              <div className="mb-4">
                <label className="block text-sm font-medium text-gray-700">Correct Answer(s)</label>
                {values.options.map((option, index) => (
                  <div key={index} className="flex items-center gap-2">
                    {values.questionType === "single" ? (
                      <Field
                        type="radio"
                        name="correctAnswer"
                        value={option}
                        className="w-4 h-4"
                      />
                    ) : (
                      <input
                        type="checkbox"
                        value={option}
                        checked={values.correctAnswer.split(",").includes(option)}
                        onChange={(e) => {
                          let newValue = e.target.checked
                            ? `${values.correctAnswer},${option}`
                            : values.correctAnswer
                                .split(",")
                                .filter((ans) => ans !== option)
                                .join(",");
                          // Remove leading comma if exists
                          if (newValue.startsWith(",")) {
                            newValue = newValue.slice(1);
                          }
                          setFieldValue("correctAnswer", newValue);
                        }}
                        className="w-4 h-4"
                      />
                    )}
                    <span>{option}</span>
                  </div>
                ))}
                <ErrorMessage name="correctAnswer" component="div" className="text-red-500 text-sm" />
              </div>

              {/* Submit Button */}
              <div className="mt-6 text-right">
                <button
                  type="submit"
                  disabled={loading}
                  className={`px-4 py-2 rounded text-white ${
                    loading ? "bg-gray-400" : "bg-teal-600 hover:bg-teal-700"
                  }`}
                >
                  {loading ? "Processing..." : isEditMode ? "Update Question" : "Create Question"}
                </button>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
}
