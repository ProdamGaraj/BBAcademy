import {useEffect, useState} from "react";

import styles from './question-radio-group.module.css'

const RadioLabelRight = (props) => {
    let [checked, setChecked] = useState(props.checked !== undefined ? props.checked : false);

    useEffect(() => {
        if (props.checked !== undefined && props.checked !== checked) {
            setChecked(props.checked)
        }
    }, [props.checked])

    return (
        <div className={styles.answerRow}>
            <input className={styles.radioDot}
                   type="radio"
                   name={props.name}
                   checked={checked}
                   value={props.value}
                   id={`${props.name}_${props.index}`}
                   onChange={e => props.onChange(e.target.value)}
                   disabled={props.disabled}/>
            <label htmlFor={`${props.name}_${props.index}`}
                   className={styles.radioText + (checked ? (' ' + styles.selectedText) : '')}>
                {props.label}
            </label>
        </div>
    )
}

export default (props) => {
    const [selectedIndex, setSelectedIndex] = useState(props.selectedIndex)

    useEffect(() => {
        if (props.selectedIndex !== selectedIndex) {
            setSelectedIndex(props.selectedIndex)
        }
    }, [props.selectedIndex])

    const onChange = (val) => {
        props.onSelectedIndexChange(~~val)
    }

    return (
        <>
            {
                props.options.map((option, i) =>
                    <RadioLabelRight
                        key={i}
                        index={i}
                        checked={i === selectedIndex} label={option} value={`${i}`}
                        onChange={onChange} {...props}/>
                )
            }
        </>
    )
}