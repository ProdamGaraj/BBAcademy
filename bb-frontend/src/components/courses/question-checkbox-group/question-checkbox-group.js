import {useEffect, useState} from "react";

import styles from './question-checkbox-group.module.css'

const CheckboxLabelRight = (props) => {
    let [checked, setChecked] = useState(props.checked !== undefined ? props.checked : false);

    useEffect(() => {
        if (props.checked !== undefined && props.checked !== checked) {
            setChecked(props.checked)
        }
    }, [props.checked])

    return (
        <div className={styles.answerRow}>
            <input className={styles.checkDot}
                   type="checkbox"
                   name={props.name}
                   checked={checked}
                   value={props.value}
                   id={`${props.name}_${props.index}`}
                   onChange={e => props.onChange(e.target.checked)}
                   disabled={props.disabled}/>
            <label htmlFor={`${props.name}_${props.index}`}
                   className={styles.checkText + (checked ? (' ' + styles.selectedText) : '')}>
                {props.label}
            </label>
        </div>
    )
}

export default (props) => {
    const [selectedIndices, setSelectedIndices] = useState(props.selectedIndices)

    useEffect(() => {
        setSelectedIndices(props.selectedIndices)
    }, [props.selectedIndices])

    const onChange = (index, val) => {
        if (val) {
            let indices = [...selectedIndices, index]
            props.onSelectedIndicesChange(indices);
        }
        else {
            let indices = selectedIndices.filter(i => i !== index)
            props.onSelectedIndicesChange(indices);
        }
    }

    return (
        <>
            {
                props.options.map((option, i) =>
                    <CheckboxLabelRight
                        key={i}
                        index={i}
                        checked={selectedIndices.includes(i)} label={option} value={`${i}`}
                        onChange={(val) => onChange(i, val)} {...props}/>
                )
            }
        </>
    )
}