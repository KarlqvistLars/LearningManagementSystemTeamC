import { useEffect, useState } from "react";
import type { UserSimplified } from "../../users/types/types";
import { FormInput } from "../../../shared/components/FormInput";
import { Button } from "../../../shared/components/Button";
import { FormLabel } from "../../../shared/components/FormLabel";
import { useNavigate } from "react-router";

export interface CourseFormData {
  name: string;
  description: string;
  startDate: string;
  endDate: string;
  mentors?: string[];
  students?: string[];
}

interface CourseFormProps {
  values?: CourseFormData;
  onSubmit: (data: CourseFormData) => void | Promise<void>;
  submitLabel?: string;
  mentorsAll?: UserSimplified[];
  studentsAll?: UserSimplified[];
}

export function CourseForm({
  values,
  onSubmit,
  submitLabel = "Submit course",
  mentorsAll,
  studentsAll,
}: CourseFormProps) {
  const [name, setName] = useState(values?.name ?? "");
  const [description, setDescription] = useState(values?.description ?? "");
  const [startDate, setStartDate] = useState(values ? values.startDate : "");
  const [endDate, setEndDate] = useState(values ? values.endDate : "");
  const [mentors, setMentors] = useState(values?.mentors ?? []);
  const [students, setStudents] = useState(values?.students ?? []);
  const navigate = useNavigate();

  function formateDateForInput(date: string | Date): string {
    const d = new Date(date);

    const year = d.getFullYear();
    const month = String(d.getMonth() + 1).padStart(2, "0");
    const day = String(d.getDate()).padStart(2, "0");

    return `${year}-${month}-${day}`;
  }

  useEffect(() => {
    setName(values?.name ?? "");
    setDescription(values?.description ?? "");
    setStartDate(values ? formateDateForInput(values.startDate) : "");
    setEndDate(values ? formateDateForInput(values.endDate) : "");
    setMentors(values?.mentors ?? []);
    setStudents(values?.students ?? []);
  }, [values]);

  const handleSubmit = async (event: React.SubmitEvent<HTMLFormElement>) => {
    event.preventDefault();

    await onSubmit({
      name,
      description,
      startDate,
      endDate,
      mentors,
      students,
    });
  };

  return (
    <form onSubmit={handleSubmit} className="flex flex-1 flex-col">
      <div className="grid grid-cols-2 gap-x-10 gap-y-5">
        <div>
          <FormLabel htmlFor="courseName" className="text-white">
            Course name
          </FormLabel>

          <FormInput
            id="courseName"
            name="courseName"
            value={name}
            required
            onChange={(event) => setName(event.target.value)}
          />
        </div>

        {mentorsAll && (
          <div className="row-span-2">
            <FormLabel htmlFor="mentor" className="text-white">
              Mentor (save not implemented)
            </FormLabel>

            <select
              id="mentor"
              name="mentor"
              multiple
              value={mentors}
              onChange={(event) =>
                setMentors(
                  Array.from(
                    event.target.selectedOptions,
                    (option) => option.value,
                  ),
                )
              }
              className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
            >
              {mentorsAll.map((mentor) => (
                <option
                  key={mentor.id}
                  value={mentor.id}
                  className="checked:bg-menu checked:text-white p-2 mt-1"
                >
                  {`${mentor.firstName} ${mentor.lastName}`}
                </option>
              ))}
            </select>
          </div>
        )}

        <div>
          <FormLabel htmlFor="startDate" className="text-white">
            Start date
          </FormLabel>

          <FormInput
            id="startDate"
            name="startDate"
            type="date"
            value={startDate}
            required
            onChange={(event) => setStartDate(event.target.value)}
            onKeyDown={(event) => {
              if (/^\d$/.test(event.key)) {
                event.preventDefault();
              }
            }}
          />
        </div>

        <div>
          <FormLabel htmlFor="endDate" className="text-white">
            End date
          </FormLabel>

          <FormInput
            id="endDate"
            name="endDate"
            type="date"
            value={endDate}
            required
            onChange={(event) => setEndDate(event.target.value)}
            onKeyDown={(event) => {
              if (/^\d$/.test(event.key)) {
                event.preventDefault();
              }
            }}
          />
        </div>

        {studentsAll && (
          <div className="row-span-4">
            <FormLabel htmlFor="students" className="text-white">
              Students
            </FormLabel>

            <select
              id="students"
              name="students"
              multiple
              value={students}
              onChange={(event) =>
                setStudents(
                  Array.from(
                    event.target.selectedOptions,
                    (option) => option.value,
                  ),
                )
              }
              className="w-full rounded-md bg-form-input px-4 py-3 text-primary-display-text"
            >
              {studentsAll.map((student) => (
                <option
                  key={student.id}
                  value={String(student.id)}
                  className="checked:bg-menu checked:text-white p-2 mt-1"
                >
                  {`${student.firstName} ${student.lastName}`}
                </option>
              ))}
            </select>

            <div className="mt-6">
              <span className="text-white mb-1 block text-base font-medium">
                Number of students
              </span>
              <span className="text-3xl text-primary-display-text">
                {students.length.toString()}
              </span>
            </div>
          </div>
        )}

        <div className="row-span-2">
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
      </div>

      <div className="mt-auto flex justify-center gap-4 pt-10">
        <Button type="submit" variant="list" color="edit">
          {submitLabel}
        </Button>
        <Button
          type="button"
          variant="list"
          color="cancel"
          onClick={() => navigate(-1)}
        >
          Back
        </Button>
      </div>
    </form>
  );
}
