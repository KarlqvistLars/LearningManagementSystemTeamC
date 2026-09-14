import { useEffect, useState } from "react";
import type { Module, CreateModule, EditModule } from "../types";
import { createModule, editModule } from "../api/modules";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";

interface ModuleFormProps {
  courseId: string;
  module?: Module;
  onModuleSaved: () => void;
}

export function ModuleForm({
  courseId,
  module,
  onModuleSaved,
}: ModuleFormProps) {
  const [name, setName] = useState("");
  const [description, setDescription] = useState("");
  const [startDate, setStartDate] = useState("");
  const [endDate, setEndDate] = useState("");

  const [message, setMessage] = useState("");
  const [error, setError] = useState("");
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();

    setMessage("");
    setError("");
    setIsSubmitting(true);

    try {
      if (module) {
        const edit: EditModule = {
          id: module.id,
          name,
          description,
          startDate: new Date(startDate),
          endDate: new Date(endDate),
          courseId,
        };

        await editModule(edit);

        setMessage("Module updated successfully!");
      } else {
        const create: CreateModule = {
          name,
          description,
          startDate: new Date(startDate),
          endDate: new Date(endDate),
          courseId,
        };

        await createModule(create);

        setMessage("Module created successfully!");
      }

      // Clears the form
      setName("");
      setDescription("");
      setStartDate("");
      setEndDate("");
      onModuleSaved();
    } catch (error) {
      console.error(error);
      setError(module ? "Couldn't update module." : "Couldn't create module.");
    } finally {
      setIsSubmitting(false);
    }
  };

  function formateDateForInput(date: string | Date) {
    return new Date(date).toISOString().split("T")[0];
  }

  useEffect(() => {
    if (module) {
      setName(module.moduleName);
      setDescription(module.description);
      setStartDate(formateDateForInput(module.startDate));
      setEndDate(formateDateForInput(module.endDate));
    } else {
      setName("");
      setDescription("");
      setStartDate("");
      setEndDate("");
    }
  }, [module]);

  return (
    <div>
      <form onSubmit={handleSubmit} className="mt-6 space-y-5">
        {/* Name */}
        <div>
          <FormLabel htmlFor="name">Name</FormLabel>

          <FormInput
            id="name"
            type="text"
            value={name}
            required
            onChange={(event) => setName(event.target.value)}
            placeholder="Enter Module Name"
          />
        </div>

        {/* Description */}
        <div>
          <FormLabel htmlFor="description">Description</FormLabel>

          <textarea
            id="description"
            value={description}
            onChange={(event) => setDescription(event.target.value)}
            required
            rows={5}
            placeholder="Enter module description"
            className="w-full resize-none rounded-lg border border-gray-300
                        bg-white px-4 py-2.5 text-gray-900 outline-none transition placeholder:text-gray-400
                        focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
          ></textarea>
        </div>

        {/* StartDate */}
        <div>
          <FormLabel htmlFor="startDate">Start date</FormLabel>

          <FormInput
            id="startDate"
            type="date"
            value={startDate}
            onChange={(event) => setStartDate(event.target.value)}
            required
            placeholder="Start date"
            className="w-full resize-none rounded-lg border border-gray-300 px-4 py-3
                        outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"
          />
        </div>

        {/* EndDate */}
        <div>
          <FormLabel htmlFor="endDate">End date</FormLabel>

          <FormInput
            id="endDate"
            type="date"
            value={endDate}
            onChange={(event) => setEndDate(event.target.value)}
            required
            placeholder="Write your comment..."
          />
        </div>

        {/* Submit */}
        <Button type="submit" disabled={isSubmitting}>
          {isSubmitting ? "Submitting..." : "Submit module"}
        </Button>

        {/* Success */}
        {message && (
          <p className="rounded-lg bg-green-100 p-3 text-green-700">
            {message}
          </p>
        )}

        {/* Error */}
        {error && (
          <p className="rounded-lg bg-red-100 p-3 text-red-700">{error}</p>
        )}
      </form>
    </div>
  );
}
