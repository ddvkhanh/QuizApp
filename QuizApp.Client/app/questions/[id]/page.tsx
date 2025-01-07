"use client";

import QuestionsForm from "components/QuestionsForm";
import { useParams } from "next/navigation";

export default function EditQuestionPage(){
    const {id} = useParams();
    if (!id) {
        return <div>Loading...</div>;
    }
    return <QuestionsForm id={id as string} />;
}
