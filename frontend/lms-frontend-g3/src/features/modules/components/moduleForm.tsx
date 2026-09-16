import { useEffect, useState } from "react";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";
import { useNavigate } from "react-router";

export interface ModuleFormData {
  name: string;
  description: string;
  startDate: Date;
  endDate: Date;
}

interface ModuleFormProps {
  values?: ModuleFormData;
  onSubmit: (data: ModuleFormData) => void | Promise<void>;
  submitLabel?: string;
}

export function ModuleForm({values, onSubmit, submitLabel = "Submit module" }: ModuleFormProps) {
  const [name, setName] = useState(values?.name ?? "");
  const [description, setDescription] = useState(values?.description ?? "");
  const [startDate, setStartDate] = useState(values ? formateDateForInput(values.startDate) : "");
  const [endDate, setEndDate] = useState(values ? formateDateForInput(values.endDate) : "");
  const navigate = useNavigate();

  
  function formateDateForInput(date: string | Date) {
    return new Date(date).toISOString().split("T")[0];
  }

  useEffect(() => {
      setName(values?.name ?? "");
      setDescription(values?.description ?? "");
      setStartDate(values ? formateDateForInput(values.startDate) : "");
      setEndDate(values ? formateDateForInput(values.endDate) : "");
  }, [values]);

  const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();

    await onSubmit({
      name,
      description,
      startDate: new Date(startDate),
      endDate: new Date(endDate),
    });
     
  };


  return (
    <div>
      <form onSubmit={handleSubmit} className="flex flex-1 flex-col">
        {/* Name */}
        <div className="grid grid-cols-1 gap-x-10 gap-y-5">
          <div>
          <FormLabel htmlFor="name" className="text-white">
            Name
          </FormLabel>

          <FormInput
            id="name"
            type="text"
            value={name}
            required
            onChange={(event) => setName(event.target.value)}
            placeholder="Enter Module Name"
          />

        </div>
        {/* StartDate */}
        <div>
          <FormLabel htmlFor="startDate" className="text-white">
            Start date
          </FormLabel>

          <FormInput
            id="startDate"
            type="date"
            value={startDate}
            onChange={(event) => setStartDate(event.target.value)}
            required
            placeholder="Start date"
          />
        </div>

        {/* EndDate */}
        <div>
          <FormLabel htmlFor="endDate" className="text-white">
            End date
          </FormLabel>

          <FormInput
            id="endDate"
            type="date"
            value={endDate}
            onChange={(event) => setEndDate(event.target.value)}
            required
            placeholder="Write your comment..."
          />
        </div>

        {/* Description */}
        <div>
          <FormLabel htmlFor="description" className="text-white">
            Description
          </FormLabel>

          <textarea
            id="description"
            value={description}
            onChange={(event) => setDescription(event.target.value)}
            required
            rows={5}
            placeholder="Enter module description"
            className="w-full resize-none rounded-md bg-form-input px-4 py-3 text-primary-display-text"
          ></textarea>
        </div>
        <div className="mt-auto flex justify-center gap-4 pt-10">
          {/* Submit */}
          <Button 
            type="submit"
            variant="list"
            color="edit">
            <label>{submitLabel}</label>
          </Button>

          {/* Cancel */}
          <Button 
            type="button"
            variant="list"
            color="cancel"
            onClick={() => navigate(-1)}>
            Back
          </Button>

        </div>
        </div>
      </form>
    </div>
  );
}
