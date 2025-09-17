# Name
API helath is up

# Purpose
Validate the API health endpoint reports 'up'.

# Steps
1. Action: Call /health
 - Instruction: Send GET /health
2. Assert: Status code
 - Instruction: Response status is 200
3. Assert: Payload
 - Instruction: JSON contains { "status": "up" }

