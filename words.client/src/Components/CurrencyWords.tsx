import { FC, useState } from "react";
import Style from "./CurrencyWords.module.scss";
import Input from "../ui/Input";
import Button from "../ui/Button";

const CurrencyWords: FC = () => {
  const [input, setInput] = useState("");
  const [result, setResult] = useState("");
  const [error, setError] = useState("");

  const handleInputChange = (value: string) => {
    setInput(value);
  };

  const handleError = () => {
    setError("Sorry, Something went wrong");
    setResult("");
  };
  const handleProcessing = async () => {
    if (error) setError("");

    const value = input.trim();
    if (!value) return;

    try {
      await fetch("https://localhost:7172/Currency?number=" + input)
        .then((response) => {
          if (response.status === 200) return response.json();
        })
        .then((data) => setResult(data.words))
        .catch(() => handleError());
    } catch {
      handleError();
    }
  };

  return (
    <>
      <h1>Currency translation (English/Dollars)</h1>
      <p>
        Enter any number in the input below to convert it to words. It currently
        supports units upto trillions of dollars.
      </p>

      <div className={Style.content}>
        <Input
          label="Currency"
          value={input}
          required
          onChange={handleInputChange}
        />
        <Button onClick={handleProcessing}>Convert</Button>

        {error && <p>{error}</p>}
      </div>

      <div>
        <p>Output</p>

        <div className={Style.result}>
          <p>{result}</p>
        </div>
      </div>
    </>
  );
};

export default CurrencyWords;
