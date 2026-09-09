import { useEffect, useState } from "react";
import type { Module, CreateModule, EditModule } from "../types";
import { createModule, editModule } from "../api/modules";

interface ModuleFormProps {
    courseId: string;
    module?: Module;
    onModuleSaved: () => void;
}

export function ModuleForm({ courseId, module, onModuleSaved }: ModuleFormProps) {
    const [name, setName] = useState("");
    const [description, setDescription] = useState("");
    const [startDate, setStartDate] = useState("");
    const [endDate, setEndDate] = useState(""); 

    const [message, setMessage] = useState("");
    const [error, setError] = useState("");
    const [isSubmitting, setIsSubmitting] = useState(false);

    const handleSubmit = async (
        event: React.SubmitEvent<HTMLFormElement>
    ) => {
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

        }else {
            const create: CreateModule = {
                name,
                description,
                startDate: new Date(startDate),
                endDate: new Date(endDate),
                courseId
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

            <form
                onSubmit={handleSubmit}
                className="mt-6 space-y-5"
            >
                {/* Name */}
                <div>
                    <label
                        htmlFor="name"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Name
                    </label>

                    <input
                        id="name"
                        type="text"
                        value={name}
                        required
                        onChange={(event) => setName(event.target.value)} 
                        placeholder="Enter Module Name"
                        className="w-full rounded-lg border border-gray-300 px-4 py-2.5
                        outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Description */}
                <div>
                    <label
                        htmlFor="description"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Description
                    </label>

                    <textarea
                        id="description"
                        value={description}
                        onChange={(event) => setDescription(event.target.value)}
                        required
                        rows={5}
                        placeholder="Enter module description"
                        className="w-full resize-none rounded-lg border border-gray-300
                        bg-white px-4 py-2.5 text-gray-900 outline-none transition placeholder:text-gray-400
                        focus:border-blue-500 focus:ring-2 focus:ring-blue-200">
                    </textarea>
                </div>

                {/* StartDate */}
                <div>
                    <label
                        htmlFor="startDate"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        Start date
                    </label>

                    <input
                        id="startDate"
                        type="date"
                        value={startDate}
                        onChange={(event) => setStartDate(event.target.value)} 
                        required
                        placeholder="Start date"
                        className="w-full resize-none rounded-lg border border-gray-300 px-4 py-3
                        outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* EndDate */}
                <div>
                    <label
                        htmlFor="endDate"
                        className="mb-2 block text-sm font-medium text-gray-700">
                        End date
                    </label>

                    <input
                        id="endDate"
                        type="date"
                        value={endDate}
                        onChange={(event) => setEndDate(event.target.value)} 
                        required
                        placeholder="Write your comment..."
                        className="w-full resize-none rounded-lg border border-gray-300 px-4 py-3
                        outline-none transition focus:border-blue-500 focus:ring-2 focus:ring-blue-200"/>
                </div>

                {/* Submit */}
                <button
                    type="submit"
                    disabled={isSubmitting}
                    className="rounded-lg bg-blue-600 px-5 py-2.5 
                    font-medium text-white transition hover:bg-blue-700 disabled:cursor-not-allowed disabled:bg-blue-300">
                    {isSubmitting ? "Submitting..." : "Submit module"}
                </button>

                {/* Success */}
                {message && (
                    <p className="rounded-lg bg-green-100 p-3 text-green-700">
                        {message}
                    </p>
                )}

                {/* Error */}
                {error && (
                    <p className="rounded-lg bg-red-100 p-3 text-red-700">
                        {error}
                    </p>
                )}
            </form>
        </div>
    );
}