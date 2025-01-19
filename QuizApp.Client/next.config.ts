import type { NextConfig } from "next";

const nextConfig: NextConfig = {
  experimental: {
    optimizePackageImports: ["@chakra-ui/react"],
  },
  env :{
    AUTH0_LOG_LEVEL: 'debug',
  }
  /* config options here */
};

export default nextConfig;
