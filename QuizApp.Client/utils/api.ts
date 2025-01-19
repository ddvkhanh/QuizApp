import axios from "axios";

const axiosInstance = axios.create({
    baseURL: process.env.NEXT_PUBLIC_API_BASE_URL,
    headers: {
      "Content-Type": "application/json",
    },
  });

export const apiRequest = async <T>(endpoint: string, method: "GET" | "POST" | "PUT" | "DELETE", body?: any): Promise<T> => {
    try {
        const response = await axiosInstance({
            url: endpoint,
            method,
            data: body,
        });
        
        return response.data as T;
    } catch (error: any) {
        console.error("API Request Error:", error?.response?.data || error.message);
        throw new Error(error?.reponse?.data?.message || "API request failed");
    }      
}
