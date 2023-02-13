import styles from './exam-content-view.module.css'
import QuestionRadioGroup from "../question-radio-group/question-radio-group";
import QuestionCheckboxGroup from "../question-checkbox-group/question-checkbox-group";
import translations from "../../../translations";
import {useContext} from "react";
import LangContext from "../../../contexts/lang-context";

const SINGLE_ANSWER = 1;
const MULTI_ANSWER = 2;

export default ({exam, onUpdated, onSubmitExam}) => {
    let lang = useContext(LangContext).lang

    const onSingleAnswerIndexChange = (question, answerIndex) => {
        let duplicate = {
            ...exam,
            questions: exam.questions.map(q => q.id === question.id ? ({
                ...q,
                selectedAnswerIndices: [answerIndex]
            }) : q)
        }
        onUpdated(duplicate)
    };
    const onMultiAnswerIndicesChange = (question, answerIndices) => {
        // console.log('multi answer change: ', answerIndices)
        let duplicate = {
            ...exam,
            questions: exam.questions.map(q => q.id === question.id ? ({
                ...q,
                selectedAnswerIndices: answerIndices
            }) : q)
        }
        onUpdated(duplicate)
    };

    return (
        <>
            <div className={styles.contentHeading}>
                {exam.title}
            </div>
            <div className={styles.contentContainer}>
                {exam.questions.map((q, i) =>
                    <div className={styles.questionBlock} key={i}>
                        <div className={styles.contentText}>
                            {q.title}
                        </div>
                        {q.mediaPath !== null ?
                            <div className={styles.contentMedia}>
                                <img src={q.mediaPath} alt=""/>
                            </div>
                            : ''
                        }

                        {q.questionType === SINGLE_ANSWER ?
                            <div className={styles.answerOptionsWrapper}>
                                <QuestionRadioGroup
                                    selectedIndex={q.selectedAnswerIndices !== undefined ? q.selectedAnswerIndices[0] : undefined}
                                    name={`${q.id}-answers`}
                                    options={q.answerOptions.map(a => a.title)}
                                    onSelectedIndexChange={i => onSingleAnswerIndexChange(q, i)}
                                />
                            </div>
                            :
                            <div className={styles.answerOptionsWrapper}>
                                <QuestionCheckboxGroup
                                    selectedIndices={q.selectedAnswerIndices ?? []}
                                    name={`${q.id}-answers`}
                                    options={q.answerOptions.map(a => a.title)}
                                    onSelectedIndicesChange={i => onMultiAnswerIndicesChange(q, i)}
                                />
                            </div>
                        }
                    </div>
                )}
                <div className={styles.submitExamButton}
                     onClick={() => onSubmitExam()}>
                    <span>{(translations[lang].send)}</span>
                </div>
            </div>
        </>
    )
}